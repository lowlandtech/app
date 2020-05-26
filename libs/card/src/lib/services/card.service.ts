import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, CardData, CardComment } from '@lowlandtech/api';
import { map } from 'rxjs/operators';

@Injectable()
export class CardService {
  constructor(private apiService: ApiService) {}

  get(slug: string): Observable<CardData> {
    return this.apiService.get('/cards/' + slug).pipe(map((data: any) => data.card));
  }

  getComments(slug: string): Observable<CardComment[]> {
    return this.apiService.get(`/cards/${slug}/comments`).pipe(map((data: any) => data.comments));
  }

  deleteCard(slug: string) {
    return this.apiService.delete('/cards/' + slug);
  }

  deleteComment(commentId: number, slug: string) {
    return this.apiService.delete(`/cards/${slug}/comments/${commentId}`);
  }

  addComment(slug, payload = ''): Observable<CardComment> {
    return this.apiService
      .post(`/cards/${slug}/comments`, { comment: { body: payload } })
      .pipe(map(data => data.comment));
  }
}
