import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, CardData } from '@lowlandtech/api';
import { map } from 'rxjs/operators';

@Injectable()
export class EditorService {
  constructor(private apiService: ApiService) {}

  publishCard(card): Observable<CardData> {
    if (card.slug) {
      return this.apiService.put('/cards/' + card.slug, { card: card }).pipe(map(data => data.card));
    }
    return this.apiService.post('/cards/', { card: card }).pipe(map(data => data.card));
  }

  get(slug: string): Observable<CardData> {
    return this.apiService.get('/cards/' + slug).pipe(map((data: any) => data.card));
  }
}
