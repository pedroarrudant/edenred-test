import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InsertClientRequest } from '../models/insert-client-request';
import { environment } from '../../environments/environment';
import { Client } from '../models/client';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/clients`;

  create(request: InsertClientRequest): Observable<unknown> {
    return this.http.post<unknown>(this.apiUrl, request);
  }

    getAll(): Observable<Client[]> {
    return this.http.get<Client[]>(this.apiUrl);
    }  
}