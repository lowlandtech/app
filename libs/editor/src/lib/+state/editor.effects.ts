import { NgrxFormsFacade, setErrors } from '@lowlandtech/ngrx-forms';
import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map, withLatestFrom } from 'rxjs/operators';

import { EditorService } from '../editor.service';
import * as EditorActions from './editor.actions';
import { go } from '@lowlandtech/ngrx-router';

@Injectable()
export class EditorEffects {
  publishCard$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EditorActions.publishCard),
      withLatestFrom(this.ngrxFormsFacade.data$),
      concatMap(([_, data]) =>
        this.editorService.publishCard(data).pipe(
          // TODO dispatch this action from the router facade when you refactor
          map(result =>
            go({
              to: { path: ['card', result.slug] },
            }),
          ),
          catchError(result => of(setErrors({ errors: result.error.errors }))),
        ),
      ),
    ),
  );

  loadCard$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EditorActions.loadCard),
      concatMap(action =>
        this.editorService.get(action.id).pipe(
          map(card => EditorActions.loadCardSuccess({ card })),
          catchError(error => of(EditorActions.loadCardFail(error))),
        ),
      ),
    ),
  );

  constructor(
    private actions$: Actions,
    private ngrxFormsFacade: NgrxFormsFacade,
    private editorService: EditorService,
  ) {}
}
