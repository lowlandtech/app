import { ApiService, CardData } from '@lowlandtech/api';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CardListConfig } from './+state/card-list.reducer';

@Injectable()
export class CardListService {
  constructor(private apiService: ApiService) {}

  query(config: CardListConfig): Observable<{ cards: CardData[]; cardsCount: number }> {
    return this.apiService.get(
      '/cards' + (config.type === 'FEED' ? '/feed' : ''),
      this.toHttpParams(config.filters),
    );
  }

  private toHttpParams(params) {
    return Object.getOwnPropertyNames(params).reduce((p, key) => p.set(key, params[key]), new HttpParams());
  }
}
