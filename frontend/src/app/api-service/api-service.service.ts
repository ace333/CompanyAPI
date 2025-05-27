import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyQuery } from '../model/company-query.model';
import { HttpClient } from '@angular/common/http';
import { Company } from '../model/company.model';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly url = `${environment.apiUrl}/company`;

  constructor(private http: HttpClient) {}

  getCompanies(limit: number, offset: number): Observable<CompanyQuery> {
    const params = { limit, offset };

    return this.http.get<CompanyQuery>(this.url, { params }).pipe(
      catchError((error) => {
        throw error;
      })
    );
  }

  getCompanyById(id: number): Observable<Company> {
    return this.http.get<Company>(`${this.url}/${id}`).pipe(
      catchError((error) => {
        throw error;
      })
    );
  }

  getCompanyByIsin(isin: string): Observable<Company> {
    return this.http.get<Company>(`${this.url}/isin/${isin}`).pipe(
      catchError((error) => {
        throw error;
      })
    );
  }

  updateCompany(company: Company, id: number): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, company).pipe(
      catchError((error) => {
        throw error;
      })
    );
  }

  createCompany(company: Company): Observable<void> {
    return this.http.post<void>(this.url, company).pipe(
      catchError((error) => {
        throw error;
      })
    );
  }
}
