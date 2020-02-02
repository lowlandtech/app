import { TestBed, async } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { provideMockActions } from '@ngrx/effects/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { AsideEffects } from './aside.effects';
import { LoadAside, AsideLoaded } from './aside.actions';

describe('AsideEffects', () => {
  let actions: Observable<any>;
  let effects: AsideEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        NxModule.forRoot(),
        StoreModule.forRoot({}),
        EffectsModule.forRoot([])
      ],
      providers: [
        AsideEffects,
        DataPersistence,
        provideMockActions(() => actions)
      ]
    });

    effects = TestBed.get(AsideEffects);
  });

  describe('loadAside$', () => {
    it('should work', () => {
      actions = hot('-a-|', { a: new LoadAside() });
      expect(effects.loadAside$).toBeObservable(
        hot('-a-|', { a: new AsideLoaded([]) })
      );
    });
  });
});
