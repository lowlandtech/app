import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { DataPersistence } from '@nrwl/angular';

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
        // Your custom REST 'toggle' logic goes here. For now just return an empty list...
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
    private dataPersistence: DataPersistence<AsidePartialState>
  ) {}
}
