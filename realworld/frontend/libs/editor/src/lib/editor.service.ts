import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ArticleData } from '@spotacard/api';
import { map } from 'rxjs/operators';

@Injectable()
export class EditorService {
  constructor(private apiService: ApiService) {}

  publishArticle(card): Observable<ArticleData> {
    if (card.slug) {
      return this.apiService.put('/articles/' + card.slug, { card: card }).pipe(map(data => data.card));
    }
    return this.apiService.post('/articles/', { card: card }).pipe(map(data => data.card));
  }

  get(slug: string): Observable<ArticleData> {
    return this.apiService.get('/articles/' + slug).pipe(map((data: any) => data.card));
  }
}
