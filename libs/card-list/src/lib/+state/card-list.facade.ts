import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import { CardListState, CardListConfig } from './card-list.reducer';
import { cardListQuery } from './card-list.selectors';
import * as CardListActions from './card-list.actions';
import { withLatestFrom, map } from 'rxjs/operators';
import { go } from '@lowlandtech/ngrx-router';
@Injectable()
export class CardListFacade {
  cards$ = this.store.select(cardListQuery.getCards);
  listConfig$ = this.store.select(cardListQuery.getListConfig);
  isLoading$ = this.store.select(cardListQuery.isLoading);
  cardsCount$ = this.store.select(cardListQuery.getCardsCount);
  totalPages$ = this.cardsCount$.pipe(
    withLatestFrom(this.listConfig$),
    map(([cardsCount, config]) => {
      return Array.from(new Array(Math.ceil(cardsCount / config.filters.limit)), (val, index) => index + 1);
    }),
  );

  constructor(private store: Store<CardListState>) {}

  favorite(slug: string) {
    this.store.dispatch(CardListActions.favorite({ slug }));
  }

  unFavorite(slug: string) {
    this.store.dispatch(CardListActions.unFavorite({ slug }));
  }

  navigateToCard(slug: string) {
    this.store.dispatch(go({ to: { path: ['/card', slug] } }));
  }

  setPage(page: number) {
    this.store.dispatch(CardListActions.setListPage({ page }));
  }

  setListConfig(config: CardListConfig) {
    this.store.dispatch(CardListActions.setListConfig({ config }));
  }
}
