import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    // Active la détection des changements avec optimisation des événements
    provideZoneChangeDetection({ eventCoalescing: true }),

    // Fournit les services nécessaires pour le routage
    provideRouter(routes),

    // Fournit le client HTTP pour les requêtes API
    provideHttpClient(),
  ],
};
