import { Injectable } from '@angular/core';

import { select, Store } from '@ngrx/store';

import { AsidePartialState } from './aside.reducer';
import { asideQuery } from './aside.selectors';
import { LoadAside } from './aside.actions';

@Injectable()
export class AsideFacade {
  loaded$ = this.store.pipe(select(asideQuery.getLoaded));
  allAside$ = this.store.pipe(select(asideQuery.getAllAside));
  selectedAside$ = this.store.pipe(select(asideQuery.getSelectedAside));

  constructor(private store: Store<AsidePartialState>) {}

  loadAll() {
    this.store.dispatch(new LoadAside());
  }
}
