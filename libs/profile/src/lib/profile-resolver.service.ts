import { cardListInitialState } from '@lowlandtech/card-list';
import * as fromCardList from '@lowlandtech/card-list';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { getProfile } from './+state/profile.actions';
import { Profile } from '@lowlandtech/api';

@Injectable()
export class ProfileResolverService implements Resolve<Profile> {
  constructor(private store: Store<any>) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    const username = route.params['username'];
    this.store.dispatch(getProfile({ id: username }));
  }
}

@Injectable()
export class ProfileCardsResolverService implements Resolve<Profile> {
  constructor(private store: Store<any>) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    const username = route.params['username'];
    this.store.dispatch(
      fromCardList.setListConfig({
        config: {
          ...cardListInitialState.listConfig,
          filters: {
            ...cardListInitialState.listConfig.filters,
            author: username,
          },
        },
      }),
    );
  }
}

@Injectable()
export class ProfileFavoritesResolverService implements Resolve<Profile> {
  constructor(private store: Store<any>) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    const username = route.parent.params['username'];
    this.store.dispatch(
      fromCardList.setListConfig({
        config: {
          ...cardListInitialState.listConfig,
          filters: {
            ...cardListInitialState.listConfig.filters,
            favorited: username,
          },
        },
      }),
    );
  }
}
