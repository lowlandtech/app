import { Injectable } from '@angular/core';

import { select, Store, Action } from '@ngrx/store';

import * as actions from './card.actions';
import * as fromCard from './card.reducer';
import * as CardSelectors from './card.selectors';
import { CardStateData } from './card.models';

@Injectable()
export class CardFacade {

  cards$ = this.store.pipe(select(CardSelectors.getCards));

  constructor(private store: Store<fromCard.CardState>) {}

  public expand(card: CardStateData) {
    this.store.dispatch(actions.expand({payload: card}));
  }

  public hide(card: CardStateData) {
    this.store.dispatch(actions.hide({payload: card}));
  }

  public collapse(card: CardStateData) {
    this.store.dispatch(actions.collapse({payload: card}));
  }

  public normalize(card: CardStateData) {
    this.store.dispatch(actions.normalize({payload: card}));
  }
}
