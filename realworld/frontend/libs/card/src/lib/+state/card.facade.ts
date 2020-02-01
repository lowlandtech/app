import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import * as CardActions from './card.actions';
import { CardState } from './card.reducer';
import { cardQuery } from './card.selectors';

@Injectable()
export class CardFacade {
  card$ = this.store.select(cardQuery.getCardData);
  comments$ = this.store.select(cardQuery.getComments);
  cardLoaded$ = this.store.select(cardQuery.getCardLoaded);
  authorUsername$ = this.store.select(cardQuery.getAuthorUsername);

  constructor(private store: Store<CardState>) {}

  loadCard(slug: string) {
    this.store.dispatch(CardActions.loadCard({ slug }));
  }
  loadComments(slug: string) {
    this.store.dispatch(CardActions.loadComments({ slug }));
  }
  follow(username: string) {
    this.store.dispatch(CardActions.follow({ username }));
  }
  unfollow(username: string) {
    this.store.dispatch(CardActions.unFollow({ username }));
  }
  favorite(slug: string) {
    this.store.dispatch(CardActions.favorite({ slug }));
  }
  unfavorite(slug: string) {
    this.store.dispatch(CardActions.unFavorite({ slug }));
  }
  delete(slug: string) {
    this.store.dispatch(CardActions.deleteCard({ slug }));
  }
  deleteComment(data: { commentId: number; slug: string }) {
    this.store.dispatch(CardActions.deleteComment(data));
  }
  submit(slug: string) {
    this.store.dispatch(CardActions.addComment({ slug }));
  }
  initializeCard() {
    this.store.dispatch(CardActions.initializeCard());
  }
}
