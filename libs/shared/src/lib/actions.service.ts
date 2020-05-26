import { ApiService, Profile, CardData } from '@lowlandtech/api';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class ActionsService {
  constructor(private apiService: ApiService) {}

  followUser(username: string): Observable<Profile> {
    return this.apiService.post('/profiles/' + username + '/follow').pipe(map(data => data.profile));
  }

  unfollowUser(username: string): Observable<Profile> {
    return this.apiService.delete('/profiles/' + username + '/follow').pipe(map(data => data.profile));
  }

  favorite(slug): Observable<CardData> {
    return this.apiService.post('/cards/' + slug + '/favorite').pipe(map(data => data.card));
  }

  unfavorite(slug): Observable<CardData> {
    return this.apiService.delete('/cards/' + slug + '/favorite').pipe(map(data => data.card));
  }
}
