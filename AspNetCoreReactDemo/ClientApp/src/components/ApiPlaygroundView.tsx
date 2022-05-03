import { postDemoFilter } from '../api'
import { HttpError } from '../api/helpers'

interface props {
  className?: string
}

export default function ApiPlaygroundView({ className }: props) {
  async function testModel() {
    try {
      console.log('posting data')
      const result = await postDemoFilter({
        denyUnlessLoggedIn: true,
        someOtherField: 'testin data goes here',
      })
      console.log('post success', result)
    } catch (e) {
      if (e instanceof HttpError) {
        console.log(`failed status: ${e.status} status text: ${e.statusText} body: ${e.body}`)
      }
      console.warn('failed', e)
    }
  }

  return <div className={className}>
    playground11
    <button onClick={() => testModel()}>post</button>
  </div>
}
