import classNames from 'classnames'
import { useState } from 'react'
import { setToken, signIn, signOut } from '../api'
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

  return <div className={classNames(css.container, className)}>
    {!user && <h2>Sign In</h2>}
    {!user && <form>
      <label>Email<TextInput value={upn} onChange={e => setUpn(e.currentTarget.value)} placeholder='john.smith@example.com' /></label>
      <label>Password<TextInput type='password' value={password} onChange={e => setPassword(e.currentTarget.value)} /></label>
      <Button onClick={e => {
        e.preventDefault()
        handleSignIn()
      }}>Sign In</Button>
    </form>}
    {user && <div className={css.container}>
      Hi {user.user.firstName}
      <Button onClick={() => handleSignOut()}>Sign out</Button>
    </div>}

    {errorText && <div>{errorText}</div>}
  </div>
}
