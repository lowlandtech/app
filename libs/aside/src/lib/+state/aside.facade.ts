import { Injectable } from '@angular/core';

import { select, Store } from '@ngrx/store';

import { AsidePartialState } from './aside.reducer';
import { asideQuery } from './aside.selectors';
import { ToggleAside } from './aside.actions';

@Injectable()
export class AsideFacade {
  toggled$ = this.store.pipe(select(asideQuery.getToggled));
  constructor(private store: Store<AsidePartialState>) {}

  toggle() {
    this.store.dispatch(new ToggleAside());
  }
}
