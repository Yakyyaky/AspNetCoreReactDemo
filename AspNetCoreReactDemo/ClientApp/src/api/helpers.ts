export async function query<T>(url: string, init?: RequestInit) {
  const resp = await execute(url, init)
  return await resp.json() as T
}

export async function execute(url: string, init?: RequestInit) {
  const resp = await fetch(`${process.env.PUBLIC_URL}${url}`, init)
  await ensureSuccess(resp)
  return resp
}

export function reqInit(method: string, body?: any) {
  return {
    method,
    headers: body === undefined ? undefined : { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  }
}

export function postInit(body?: any): RequestInit {
  return reqInit('POST', body)
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
