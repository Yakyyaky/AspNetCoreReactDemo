type UserUpn = {
  upn: string
}

export type User = UserUpn & {
  firstName: string | null
  lastName: string | null
}

export type UserRegistration = {
  user: User
  password: string
} 

export type SignInCredential = UserUpn & {
  password: string
}

export type SignOutUser = UserUpn

export type AuthenticatedUser = {
  user: User
  accessToken: string
}
