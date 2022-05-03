export type User = {
  upn: string
  firstName: string | null
  lastName: string | null
}

export type UserRegistration = {
  user: User
  password: string
} 

export type SignInCredential = {
  upn: string
  password: string
}
