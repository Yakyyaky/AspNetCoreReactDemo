import classNames from 'classnames'
import { useState } from 'react'
import { registerUser, setToken, signIn, signOut } from '../api'
import { HttpError } from '../api/helpers'
import { AuthenticatedUser } from '../models/user'
import css from './AccountView.module.scss'
import Button from './Button'
import TextInput from './TextInput'

interface props {
  className?: string
}

export default function AccountView({ className }: props) {
  const [upn, setUpn] = useState('')
  const [password, setPassword] = useState('')
  const [user, setUser] = useState<AuthenticatedUser | null>(null)
  const [errorText, setErrorText] = useState('')
  const [createAccount, setCreateAccount] = useState(false)
  const [firstName, setFirstName] = useState<string>('')
  const [lastName, setLastName] = useState<string>('')

  async function handleSignIn() {
    if (!upn || !password) return

    try {
      setErrorText('')
      const result = await signIn({ upn, password })
      if (result) {
        setUser(result)
        setToken(result.accessToken)
      }
    } catch (e) {
      if (e instanceof HttpError && e.status === 401) {
        setErrorText('Invalid credentials')
      }
    }
  }

  async function handleSignOut() {
    if (!user) return

    try {
      await signOut({ upn: user.user.upn })
    } catch (e) {
      if (e instanceof HttpError) {
        setErrorText(`Warning: Sign out issue. Error Code: ${e.status}`)
      }
    }
    // don't care about result
    setUser(null)
    setToken(null)
  }

  async function handleCreateAccount() {
    if (!upn || !password || !firstName || !lastName) {
      setErrorText('Please fill in all input fields')
      return
    }

    try {
      await registerUser({
        password,
        user: {
          firstName, lastName, upn,
        },
      })
    } catch (e) {
      if (e instanceof HttpError) {
        setErrorText(`Failed to register user. Error Code: ${e.status}`)
      }
    }
    setCreateAccount(false)
  }

  return <div className={classNames(css.container, className)}>
    {!user && <h2>Sign In</h2>}
    {!user && <form>
      <label>Email<TextInput value={upn} onChange={e => setUpn(e.currentTarget.value)} placeholder='john.smith@example.com' /></label>
      <label>Password<TextInput type='password' value={password} onChange={e => setPassword(e.currentTarget.value)} /></label>
      {createAccount && <label>First Name<TextInput value={firstName} onChange={e => setFirstName(e.currentTarget.value)} required /></label>}
      {createAccount && <label>Last Name<TextInput value={lastName} onChange={e => setLastName(e.currentTarget.value)} required /></label>}
      <div className={css.buttonBar}>
        {!createAccount && <Button onClick={e => {
          e.preventDefault()
          handleSignIn()
        }}>Sign In</Button>}
        {!createAccount && <Button onClick={e => {
          e.preventDefault()
          setCreateAccount(true)
          setFirstName('')
          setLastName('')
        }}>Sign Up</Button>}
        {createAccount && <Button onClick={e => {
          e.preventDefault()
          handleCreateAccount()
        }}>Create Account</Button>}
        {createAccount && <Button onClick={e => {
          e.preventDefault()
          setCreateAccount(false)
        }}>Cancel</Button>}
      </div>
    </form>}
    {user && <div className={css.container}>
      <h2>Hi {user.user.firstName}</h2>
      <div className={css.buttonBar}>
        <Button onClick={() => handleSignOut()}>Sign out</Button>
      </div>
    </div>}

    {errorText && <div>{errorText}</div>}
  </div>
}
