export async function query<T>(url: string, init?: RequestInit) {
  const resp = await execute(url, init)
  return await resp.json() as T
}

export async function execute(url: string, init?: RequestInit) {
  const resp = await fetch(`${process.env.PUBLIC_URL}${url}`, init)
  await ensureSuccess(resp)
  return resp
}

export function reqInit(method: string, bearerToken: string | null, body?: any): RequestInit {
  const headers = new Headers()
  const init: RequestInit = {
    method,
    headers,
    body: JSON.stringify(body),
  }

  if (body) {
    headers.append('Content-Type', 'application/json')
  }

  if (bearerToken) {
    headers.append('Authorization', 'Bearer ' + bearerToken)
  }

  return init
}

export function postInit(bearerToken: string | null, body?: any): RequestInit {
  return reqInit('POST', body, bearerToken)
}

export async function ensureSuccess(resp: Response) {
  if (!resp.ok) {
    let body = null

    try {
      body = await resp.text()
    } catch (e) { }

    throw new HttpError(resp.status, resp.statusText, body)
  }
}

export class HttpError extends Error {
  public readonly status: number
  public readonly statusText: string
  public readonly body: string | null

  constructor(status: number, statusText: string, body: string | null) {
    super(`${status} ${statusText}`)

    this.name = 'HttpError'
    this.status = status
    this.statusText = statusText
    this.body = body

    Object.setPrototypeOf(this, HttpError.prototype)
  }
}
