import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate
} from '@angular/router';
import { Observable } from 'rxjs';
import { filter, take, tap } from 'rxjs/operators';
import { CardFacade } from '../+state/card.facade';

@Injectable()
export class CardGuardService implements CanActivate {
  constructor(private facade: CardFacade) {}

  waitForCardToLoad(): Observable<boolean> {
    return this.facade.cardLoaded$.pipe(
      filter(loaded => loaded),
      take(1)
    );
  }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    const slug = route.params['slug'];
    this.facade.loadCard(slug);

    return this.waitForCardToLoad().pipe(
      tap(() => this.facade.loadComments(slug))
    );
  }
}
