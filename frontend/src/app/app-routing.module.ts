import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GridComponent } from './grid/grid.component';
import { CompanyComponent } from './company/company.component';
import { CompanyNewComponent } from './company-new/company-new.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'grid',
    pathMatch: 'full',
  },
  {
    path: 'grid',
    component: GridComponent,
  },
  {
    path: 'company/:id',
    component: CompanyComponent
  },
  {
    path: 'company-new',
    component: CompanyNewComponent
  },
  {
    path: '**',
    redirectTo: 'grid',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
