import { Injectable } from '@angular/core';

import { select, Store } from '@ngrx/store';

import * as actions from './card.actions';
import * as fromCard from './card.reducer';
import * as CardSelectors from './card.selectors';

@Injectable()
export class CardFacade {

  states$ = this.store.pipe(select(CardSelectors.getStates));
  card$ = this.store.pipe(select(CardSelectors.getCard));

  getCard(cardId: string) {
    return this.store.pipe(select(CardSelectors.getCardById, {cardId}))
  }

  constructor(private store: Store<fromCard.CardsState>) {}

  public expand(cardId: string) {
    this.store.dispatch(actions.expand({payload: cardId}));
  }

  public hide(cardId: string) {
    this.store.dispatch(actions.hide({payload: cardId}));
  }

  public collapse(cardId: string) {
    this.store.dispatch(actions.collapse({payload: cardId}));
  }

  public normalize(cardId: string) {
    this.store.dispatch(actions.normalize({payload: cardId}));
  }

  public selectCard(cardId: string) {
    this.store.dispatch(actions.selectCard({payload: cardId}));
  }
}
