import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import { homeQuery } from './home.selectors';
import * as HomeActions from './home.actions';
import { HomeState } from './home.reducer';

@Injectable()
export class HomeFacade {
  home$ = this.store.select(homeQuery.getHome);
  tags$ = this.store.select(homeQuery.getTags);
  selectedTag$ = this.store.select(homeQuery.getSelectedTag);

  constructor(private store: Store<HomeState>) {}

  loadTags() {
    this.store.dispatch(HomeActions.loadTags());
  }

  selectTag(tag: string){
    this.store.dispatch(HomeActions.selectTag({tag}));
  }
}
