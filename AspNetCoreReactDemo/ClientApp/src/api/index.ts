import { ModelType } from '../models/modelType'
import { AuthenticatedUser, SignInCredential, SignOutUser, User, UserRegistration } from '../models/user'
import { execute, query, reqInit } from './helpers'

export async function postDemoFilter(modelType: ModelType) {
  return await query<ModelType>('/api/demofilter', reqInit('POST', bearerToken, modelType))
}

export async function signIn(credential: SignInCredential): Promise<AuthenticatedUser | null> {
  return await query<AuthenticatedUser | null>('/api/authentication', reqInit('POST', null, credential))
}

export async function signOut(user: SignOutUser) {
  return await query<boolean>('/api/authentication/signout', reqInit('POST', bearerToken, user))
}

export async function registerUser(registration: UserRegistration): Promise<User> {
  return await query<User>('/api/registration', reqInit('POST', null, registration))
}

let bearerToken: string | null
export function setToken(token: string | null) {
  bearerToken = token
}

export function getToken() {
  return bearerToken
}
