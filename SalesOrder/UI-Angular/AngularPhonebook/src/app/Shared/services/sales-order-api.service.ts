import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class SalesOrderApiService {
  readonly baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getEntryList(): Observable<any[]> {
    return this.http.get<any>(this.baseUrl + '/salesOrders');
  }
  
  getEntryById(id: number | string) {
    return this.http.get<any>(this.baseUrl + `/salesOrder/${id}`);
  }

  addEntry(entry:any) {
    return this.http.post(this.baseUrl + '/salesOrders', entry);
  }

  updateEntry(id: number | string, entry: any) {
    return this.http.put(this.baseUrl + `/salesOrder/${id}`, entry);
  }

  deleteEntry(id: number | string) {
    return this.http.delete(this.baseUrl + `/salesOrder/${id}`);
  }
 


}
