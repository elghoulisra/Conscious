import { Component, OnInit } from '@angular/core';
import { BrandService } from '../../services/brand.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-brand-search',
  imports: [CommonModule, FormsModule],
  templateUrl: './brand-search.component.html',
  styleUrls: ['./brand-search.component.scss']
})
export class BrandSearchComponent implements OnInit {
  brands: any[] = [];           // Liste complète des marques
  filteredBrands: any[] = [];   // Liste filtrée après la recherche
  searchTerm: string = '';      // Terme de recherche
  selectedBrandId: number | null = null;  // Marque sélectionnée pour voter
  voteReason: string = '';      // Raison du vote
  searchMessage: string = '';   // Message affiché après la recherche
  voteMessage: string = '';     // Message de confirmation ou d'erreur pour le vote
  voteInProgress: boolean = false; // Indicateur de progression du vote

  constructor(private brandService: BrandService) {}

  ngOnInit(): void {
    this.getBrands();
  }

  // Récupère toutes les marques
  getBrands(): void {
    this.brandService.getBrands().subscribe((data) => {
      this.brands = data;
      this.filteredBrands = data;  // Initialise filteredBrands avec la liste complète
    });
  }

  // Recherche les marques en fonction du terme de recherche
  searchBrand(): void {
    if (this.searchTerm.trim()) {
      this.filteredBrands = this.brands.filter((brand) =>
        brand.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
      if (this.filteredBrands.length === 0) {
        this.searchMessage = 'Aucun résultat trouvé.';
      } else {
        this.searchMessage = `${this.filteredBrands.length} résultats trouvés.`;
      }
    } else {
      this.filteredBrands = [...this.brands];
      this.searchMessage = '';
    }
  }

  // Sélectionne la marque pour voter
  selectBrandForVote(brandId: number): void {
    this.selectedBrandId = brandId;
    this.voteMessage = ''; // Réinitialise le message de vote
  }

  // Soumettre un vote pour boycotter
  submitVote(): void {
    if (this.selectedBrandId && this.voteReason.trim()) {
      this.voteInProgress = true;
      this.voteMessage = 'Vote en cours...';

      this.brandService.voteForBoycott(this.selectedBrandId, { reason: this.voteReason }).subscribe(
        (response) => {
          this.voteMessage = 'Votre vote a été pris en compte avec succès!';
          this.voteInProgress = false;
          this.selectedBrandId = null; // Réinitialiser après le vote
          this.voteReason = ''; // Réinitialiser la raison
        },
        (error) => {
          this.voteMessage = 'Une erreur est survenue, veuillez réessayer.';
          this.voteInProgress = false;
        }
      );
    } else {
      this.voteMessage = 'Veuillez sélectionner une marque et entrer une raison pour le vote.';
    }
  }
  
  highlightSearchTerm(text: string): string {
    if (!this.searchTerm.trim()) {
      return text;
    }
    const regex = new RegExp(`(${this.searchTerm})`, 'gi');
    return text.replace(regex, '<span class="bg-warning">$1</span>');
  }
  
}
