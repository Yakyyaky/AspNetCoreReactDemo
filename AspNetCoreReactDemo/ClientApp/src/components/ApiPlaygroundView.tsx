import classNames from 'classnames'
import { useState } from 'react'
import { postDemoFilter } from '../api'
import { HttpError } from '../api/helpers'
import { ModelType } from '../models/modelType'
import css from './ApiPlaygroundView.module.scss'
import Button from './Button'
import TextInput from './TextInput'
import Toggle from './Toggle'

interface props {
  className?: string
}

export default function ApiPlaygroundView({ className }: props) {
  const [hasDenyField, setHasDenyField] = useState(false)
  const [denyUnlessLoggedIn, setDenyUnlessLoggedIn] = useState(false)
  const [someOtherField, setSomeOtherField] = useState('Some other field text here')

  const [posted, setPosted] = useState('')
  const [result, setResult] = useState('')
  const [resultTime, setResultTime] = useState<Date | null>(null)

  async function testModel() {
    try {
      let data: ModelType = {
        someOtherField: someOtherField,
      }
      if (hasDenyField) {
        // only set the value if it's wanted to be sent
        data.denyUnlessLoggedIn = denyUnlessLoggedIn
      }

      const result = await postDemoFilter(data)
      setResultTime(new Date())
      setPosted(JSON.stringify(data, null, 4))
      setResult(JSON.stringify(result, null, 4))
    } catch (e) {
      if (e instanceof HttpError) {
        console.log(`failed status: ${e.status} status text: ${e.statusText} body: ${e.body}`)
        setResultTime(new Date())
        setResult(`Post failed with error ${e.status}`)
      }
      console.warn('failed', e)
    }
  }

  return <div className={classNames(css.container, className)}>
    <h2>Demo Playground</h2>
    <div className={classNames(css.container, css.group)}>
      <label>SomeOtherField<TextInput value={someOtherField} onChange={e => setSomeOtherField(e.currentTarget.value)} /></label>
      <label>Use Deny Field<Toggle value={hasDenyField} onChange={e => setHasDenyField(e)} /></label>
      <label>Deny Unless Logged In<Toggle value={denyUnlessLoggedIn} onChange={e => setDenyUnlessLoggedIn(e)} disabled={!hasDenyField} /></label>
      <Button onClick={() => testModel()}>Send</Button>
    </div>
    <div className={classNames(css.container, css.group)}>
      <div>Posted:</div>
      <pre>{posted}</pre>
    </div>
    <div className={classNames(css.container, css.group)}>
      <div>Result received at {resultTime?.toLocaleTimeString()}</div>
      <pre>{result}</pre>
    </div>
  </div>
}
