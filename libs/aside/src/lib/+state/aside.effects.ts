import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { DataPersistence } from '@nrwl/angular';

import { AsidePartialState } from './aside.reducer';
import {
  LoadAside,
  AsideLoaded,
  AsideLoadError,
  AsideActionTypes
} from './aside.actions';

@Injectable()
export class AsideEffects {
  @Effect() loadAside$ = this.dataPersistence.fetch(
    AsideActionTypes.LoadAside,
    {
      run: (action: LoadAside, state: AsidePartialState) => {
        // Your custom REST 'load' logic goes here. For now just return an empty list...
        return new AsideLoaded([]);
      },

      onError: (action: LoadAside, error) => {
        console.error('Error', error);
        return new AsideLoadError(error);
      }
    }
  );

  constructor(
    private actions$: Actions,
    private dataPersistence: DataPersistence<AsidePartialState>
  ) {}
}
