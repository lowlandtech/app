import { CardService } from '../services';
import { ActionsService } from '@lowlandtech/shared';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import {
  catchError,
  concatMap,
  exhaustMap,
  map,
  mergeMap,
  withLatestFrom
} from 'rxjs/operators';
import * as CardActions from './card.actions';

import { NgrxFormsFacade, setErrors, resetForm } from '@lowlandtech/ngrx-forms';
import { go } from '@lowlandtech/ngrx-router';
@Injectable()
export class CardEffects {
  loadCard = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.loadCard),
      concatMap(action =>
        this.cardService.get(action.slug).pipe(
          map(results => CardActions.loadCardSuccess({ card: results })),
          catchError(error => of(CardActions.loadCardFail(error)))
        )
      )
    )
  );

  loadComments = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.loadComments),
      concatMap(action =>
        this.cardService.getComments(action.slug).pipe(
          map(comments => CardActions.loadCommentsSuccess({ comments })),
          catchError(error => of(CardActions.loadCommentsFail(error)))
        )
      )
    )
  );

  deleteCard = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.deleteCard),
      concatMap(action =>
        this.cardService.deleteCard(action.slug).pipe(
          map(_ => go({ to: { path: ['/'] } })),
          catchError(error => of(CardActions.deleteCardFail(error)))
        )
      )
    )
  );

  addComment = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.addComment),
      map(action => action.slug),
      withLatestFrom(
        this.ngrxFormsFacade.data$,
        this.ngrxFormsFacade.structure$
      ),
      exhaustMap(([slug, data, structure]) =>
        this.cardService.addComment(slug, data.comment).pipe(
          mergeMap(comment => [
            CardActions.addCommentSuccess({ comment }),
            resetForm()
          ]),
          catchError(result => of(setErrors({ errors: result.error.errors })))
        )
      )
    )
  );

  deleteComment = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.deleteComment),
      concatMap(action =>
        this.cardService.deleteComment(action.commentId, action.slug).pipe(
          map(_ =>
            CardActions.deleteCommentSuccess({ commentId: action.commentId })
          ),
          catchError(error => of(CardActions.deleteCommentFail(error)))
        )
      )
    )
  );

  follow = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.follow),
      map(action => action.username),
      concatMap(username =>
        this.actionsService.followUser(username).pipe(
          map(profile => CardActions.followSuccess({ profile })),
          catchError(error => of(CardActions.followFail(error)))
        )
      )
    )
  );

  unFollow = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.unFollow),
      map(action => action.username),
      concatMap(username =>
        this.actionsService.unfollowUser(username).pipe(
          map(profile => CardActions.unFollowSuccess({ profile })),
          catchError(error => of(CardActions.unFollowFail(error)))
        )
      )
    )
  );

  favorite = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.favorite),
      map(action => action.slug),
      concatMap(slug =>
        this.actionsService.favorite(slug).pipe(
          map(card => CardActions.favoriteSuccess({ card })),
          catchError(error => of(CardActions.favoriteFail(error)))
        )
      )
    )
  );

  unFavorite = createEffect(() =>
    this.actions$.pipe(
      ofType(CardActions.unFavorite),
      map(action => action.slug),
      concatMap(slug =>
        this.actionsService.unfavorite(slug).pipe(
          map(card => CardActions.unFavoriteSuccess({ card })),
          catchError(error => of(CardActions.unFavoriteFail(error)))
        )
      )
    )
  );

  constructor(
    private actions$: Actions,
    private cardService: CardService,
    private actionsService: ActionsService,
    private ngrxFormsFacade: NgrxFormsFacade
  ) {}
}
