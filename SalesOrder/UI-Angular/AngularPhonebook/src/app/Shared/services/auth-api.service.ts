import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  readonly baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  login(userCredentials: any) {
    return this.http.post(this.baseUrl + '/auth/login', userCredentials);
  }
}
