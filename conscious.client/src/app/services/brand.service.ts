import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private apiUrl = 'http://localhost:5069/api/brands'; // URL de l'API

  constructor(private http: HttpClient) {}

  // Récupérer toutes les marques
  getBrands(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  // Vérifier si une marque est boycottée
  getBrandById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  // Voter pour boycotter une marque
  voteForBrand(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }


  voteForBoycott(brandId: number, voteData: { reason: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/${brandId}/vote`, voteData);
  }
}
