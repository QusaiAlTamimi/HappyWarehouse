export class user {
  id= "";
  fullName = "";
  active? :boolean;
  userName = "";
  email = "";
  password = "";
}

export interface LoginResponse {
  message: string | null;
  isAuthenticated: boolean;
  username: string;
  email: string;
  roles: string[];
  token: string;
  expiresOn: string;
}

export class LoginRequest {
  email: string;
  password: string;

  constructor(email: string, password: string) {
    this.email = email;
    this.password = password;
  }
}
