import { Routes } from '@angular/router';
import { BrandSearchComponent } from './components/brand-search/brand-search.component';

export const routes: Routes = [
    { path: 'search', component: BrandSearchComponent },
    { path: '', redirectTo: '/search', pathMatch: 'full' }
  ];
  
