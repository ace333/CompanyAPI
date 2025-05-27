import { Component, OnInit } from '@angular/core';
import { Company } from '../model/company.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../api-service/api-service.service';
import { catchError } from 'rxjs';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-company-new',
  templateUrl: './company-new.component.html',
  styleUrls: ['./company-new.component.scss'],
})
export class CompanyNewComponent {
  companyForm: FormGroup = this.formBuilder.group({
    isin: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    exchange: new FormControl('', Validators.required),
    ticker: new FormControl('', Validators.required),
    website: new FormControl(''),
  });

  constructor(
    private apiService: ApiService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  onCreateClick(): void {
    if (this.companyForm.valid) {
      this.apiService
        .createCompany(this.companyForm.value)
        .pipe(
          catchError((error: HttpErrorResponse) => {
            if(error.status === 400) {
              alert(error.error);
            }
            throw error;
          })
        )
        .subscribe(() => {
          this.router.navigate(['grid']);
        });
    }
  }
}
