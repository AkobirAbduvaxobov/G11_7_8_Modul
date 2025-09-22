import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RegisterDto } from "./Interfaces/register.dto";
import { LoginDto } from "./Interfaces/login.dto";
import { LoginResponseDto } from "./Interfaces/login-response-dto";

@Injectable({ providedIn: 'root' })
export class AuthApiService {
  private readonly apiUrl = 'https://localhost:7121/api/auth';

  constructor(private http: HttpClient) { }

  public register(dto: RegisterDto): Observable<number> {
    const url = `${this.apiUrl}/register`;
    return this.http.post<number>(url, dto);
  }

  public login(dto: LoginDto): Observable<LoginResponseDto> {
    const url = `${this.apiUrl}/login`;
    return this.http.post<LoginResponseDto>(url, dto);
  }
}
