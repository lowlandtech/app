import { ActionsService } from '@lowlandtech/shared';
import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map, withLatestFrom } from 'rxjs/operators';

import { CardListService } from '../card-list.service';
import * as CardListActions from './card-list.actions';

import { CardListFacade } from './card-list.facade';

@Injectable()
export class CardListEffects {
  setListPage = createEffect(() =>
    this.actions$.pipe(
      ofType(CardListActions.setListPage),
      map(() => CardListActions.loadCards()),
    ),
  );

  setListTag = createEffect(() =>
    this.actions$.pipe(
      ofType(CardListActions.setListConfig),
      map(() => CardListActions.loadCards()),
    ),
  );

  loadCards = createEffect(() =>
    this.actions$.pipe(
      ofType(CardListActions.loadCards),
      withLatestFrom(this.facade.listConfig$),
      concatMap(([_, config]) =>
        this.cardListService.query(config).pipe(
          map(results =>
            CardListActions.loadCardsSuccess({
              cards: results.cards,
              cardsCount: results.cardsCount,
            }),
          ),
          catchError(error => of(CardListActions.loadCardsFail({ error }))),
        ),
      ),
    ),
  );

  favorite = createEffect(() =>
    this.actions$.pipe(
      ofType(CardListActions.favorite),
      map(action => action.slug),
      concatMap(slug =>
        this.actionsService.favorite(slug).pipe(
          map(card => CardListActions.favoriteSuccess({ card })),
          catchError(error => of(CardListActions.favoriteFail(error))),
        ),
      ),
    ),
  );

  unFavorite = createEffect(() =>
    this.actions$.pipe(
      ofType(CardListActions.unFavorite),
      map(action => action.slug),
      concatMap(slug =>
        this.actionsService.unfavorite(slug).pipe(
          map(card => CardListActions.unFavoriteSuccess({ card })),
          catchError(error => of(CardListActions.unFavoriteFail(error))),
        ),
      ),
    ),
  );

  constructor(
    private actions$: Actions,
    private cardListService: CardListService,
    private actionsService: ActionsService,
    private facade: CardListFacade,
  ) {}
}
