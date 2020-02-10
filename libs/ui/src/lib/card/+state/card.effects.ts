import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, concatMap } from 'rxjs/operators';
import { EMPTY, of } from 'rxjs';

import * as CardActions from './card.actions';



@Injectable()
export class CardEffects {

  loadCards$ = createEffect(() => this.actions$.pipe(
    ofType(CardActions.loadCards),
    concatMap(() =>
      /** An EMPTY observable only emits completion. Replace with your own observable API request */
      EMPTY.pipe(
        map(data => CardActions.loadCardsSuccess({ data })),
        catchError(error => of(CardActions.loadCardsFailure({ error }))))
    )
  ));



  constructor(private actions$: Actions) {}

}
