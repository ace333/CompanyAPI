import { Component } from '@angular/core';
import { Observable, catchError, filter, first } from 'rxjs';
import { CompanyQuery } from '../model/company-query.model';
import { ApiService } from '../api-service/api-service.service';
import { Company } from '../model/company.model';
import { PageInfo } from '../model/page-info.model';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
})
export class GridComponent {
  displayedColumns: string[] = [
    'id',
    'isin',
    'name',
    'exchange',
    'ticker',
    'website',
  ];
  companies: Company[] = [];
  pageInfo: PageInfo = { totalRecords: 0 } as PageInfo;
  isinFormGroup: FormGroup = this.formBuilder.group({
    isin: new FormControl('', Validators.required),
  });

  constructor(
    private apiService: ApiService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.getCompanies(0, 0);
  }

  onPageChange($event: PageEvent): void {
    this.getCompanies($event.pageSize, $event.pageIndex * $event.pageSize);
  }

  onRowClick($event: Company) {
    this.router.navigate(['company', $event.id]);
  }

  onSearchClick(): void {
    this.apiService
      .getCompanyByIsin(this.isinFormGroup.get('isin')?.value)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if(error.status === 404) {
            alert('Company not found by given ISIN');
          }
          throw error;
        })
      ).subscribe((result: Company) => this.router.navigate(['company', result.id]));
  }

  private getCompanies(limit: number, offset: number): void {
    this.apiService
      .getCompanies(limit, offset)
      .pipe(
        catchError((error) => {
          throw error;
        }),
        filter((result: CompanyQuery) => !!result)
      )
      .subscribe((result: CompanyQuery) => {
        this.companies = result.items;
        this.pageInfo = result.pageInfo;
      });
  }
}
