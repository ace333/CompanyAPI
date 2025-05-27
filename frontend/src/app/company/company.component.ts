import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../api-service/api-service.service';
import { Observable, catchError, finalize, switchMap } from 'rxjs';
import { Company } from '../model/company.model';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],
})
export class CompanyComponent implements OnInit {
  company: Company = {} as Company;
  companyForm: FormGroup = this.formBuilder.group({
    isin: new FormControl(''),
    name: new FormControl(''),
    exchange: new FormControl(''),
    ticker: new FormControl(''),
    website: new FormControl(''),
  });

  constructor(
    private activatedRoute: ActivatedRoute,
    private apiService: ApiService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(switchMap((params) => this.apiService.getCompanyById(params['id'])))
      .subscribe((company: Company) => {
        this.company = company;
        this.companyForm = this.formBuilder.group({
          isin: new FormControl(company.isin, Validators.required),
          name: new FormControl(company.name, Validators.required),
          exchange: new FormControl(company.exchange, Validators.required),
          ticker: new FormControl(company.ticker, Validators.required),
          website: new FormControl(company.website),
        });
      });
  }

  onUpdateClick(): void {
    if (this.companyForm.valid) {
      this.apiService
        .updateCompany(this.companyForm.value, this.company.id)
        .pipe(
          catchError((error) => {
            throw error;
          }),
        )
        .subscribe(() => {
          window.location.reload();
        });
    }
  }
}
