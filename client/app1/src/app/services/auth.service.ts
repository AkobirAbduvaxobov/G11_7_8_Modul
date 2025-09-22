import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { EMPTY, Observable, map, tap } from 'rxjs';
import { AuthApiService } from '../api/auth-api.service';
import { RegisterModel } from './models/register.model';
import { RegisterDto } from '../api/Interfaces/register.dto';
import { LoginModel } from './models/login.model';
import { LoginDto } from '../api/Interfaces/login.dto';
import { LoginResponseDto } from '../api/Interfaces/login-response-dto';
import { LoginResponseModel } from './models/login-response-model';

@Injectable({ providedIn: 'root' })
export class AuthService {

    constructor(
        private authApiService: AuthApiService,
    ) { }

    public register(model: RegisterModel): Observable<number> {
        const dto = this.convertRegisterModelToDto(model);
        return this.authApiService.register(dto);
    }

    public login(model: LoginModel): Observable<LoginResponseModel> {
        const dto = this.convertLoginModelToDto(model);
        return this.authApiService.login(dto).pipe(
            tap(responseDto => {
                
                    localStorage.setItem('access_token', responseDto.accessToken);
                    localStorage.setItem('refresh_token', responseDto.refreshToken);
                
            }),
            map(responseDto => this.convertLoginResponseDtoToModel(responseDto))
        );
    }

    private convertRegisterModelToDto(model: RegisterModel): RegisterDto {
        return {
            firstName: model.firstName,
            lastName: model.lastName,
            userName: model.username,
            email: model.email,
            password: model.password,
        };
    }

    private convertLoginModelToDto(model: LoginModel): LoginDto {
        return {
            userName: model.userName,
            password: model.password
        };
    }

    private convertLoginResponseDtoToModel(dto: LoginResponseDto): LoginResponseModel {
        return {
            accessToken: dto.accessToken,
            refreshToken: dto.refreshToken,
            tokenType: dto.tokenType,
            expires: dto.expires
        };
    }
}
