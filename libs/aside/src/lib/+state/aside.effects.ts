import { Injectable, Inject } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { DataPersistence } from '@nrwl/angular';
import { DOCUMENT } from '@angular/common';
import { AsidePartialState } from './aside.reducer';
import {
  ToggleAside,
  AsideToggled,
  AsideToggleError,
  AsideActionTypes
} from './aside.actions';

@Injectable()
export class AsideEffects {
  @Effect() toggleAside$ = this.dataPersistence.fetch(
    AsideActionTypes.ToggleAside,
    {
      run: (action: ToggleAside, state: AsidePartialState) => {
        this.document.querySelector('body')
                     .classList.toggle('aside-menu-hidden');
        return new AsideToggled();
      },

      onError: (action: ToggleAside, error) => {
        console.error('Error', error);
        return new AsideToggleError(error);
      }
    }
  );

  constructor(
    private actions$: Actions,
    private dataPersistence: DataPersistence<AsidePartialState>,
    @Inject(DOCUMENT) private document: Document
  ) { }
}
