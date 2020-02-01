import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import * as fromCard from './+state/card.reducer';
import { filter, take, switchMap, tap } from 'rxjs/operators';
import * as fromActions from './+state/card.actions';
import { CardFacade } from './+state/card.facade';

@Injectable()
export class CardGuardService implements CanActivate {
  constructor(private facade: CardFacade) {}

  waitForCardToLoad(): Observable<boolean> {
    return this.facade.cardLoaded$.pipe(
      filter(loaded => loaded),
      take(1),
    );
  }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    const slug = route.params['slug'];
    this.facade.loadCard(slug);

    return this.waitForCardToLoad().pipe(tap(() => this.facade.loadComments(slug)));
  }
}
