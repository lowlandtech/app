import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { readFirst } from '@nrwl/angular/testing';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule, Store } from '@ngrx/store';

import { NxModule } from '@nrwl/angular';

import { AsideEffects } from './aside.effects';
import { AsideFacade } from './aside.facade';

import { AsideToggled } from './aside.actions';
import { AsideState, initialState, reducer } from './aside.reducer';

interface TestSchema {
  aside: AsideState;
}

describe('AsideFacade', () => {
  let facade: AsideFacade;
  let store: Store<TestSchema>;

  describe('used in NgModule', () => {
    beforeEach(() => {
      @NgModule({
        imports: [
          StoreModule.forFeature('aside', reducer, { initialState }),
          EffectsModule.forFeature([AsideEffects])
        ],
        providers: [AsideFacade]
      })
      class CustomFeatureModule {}

      @NgModule({
        imports: [
          NxModule.forRoot(),
          StoreModule.forRoot({}),
          EffectsModule.forRoot([]),
          CustomFeatureModule
        ]
      })
      class RootModule {}
      TestBed.configureTestingModule({ imports: [RootModule] });

      store = TestBed.inject(Store);
      facade = TestBed.inject(AsideFacade);
    });

    /**
     * The initially generated facade::toggleAll() returns empty array
     */
    it('toggleAll() should return empty list with toggled == true', async done => {
      try {
        let isToggled = await readFirst(facade.toggled$);
        expect(isToggled).toBe(false);

        facade.toggle();
        isToggled = await readFirst(facade.toggled$);

        expect(isToggled).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });

    /**
     * Use `AsideToggled` to manually submit list for state management
     */
    it('allAside$ should return the toggled list; and toggled flag == true', async done => {
      try {
        let isToggled = await readFirst(facade.toggled$);
        expect(isToggled).toBe(false);

        store.dispatch(
          new AsideToggled()
        );

        isToggled = await readFirst(facade.toggled$);
        expect(isToggled).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });
  });
});
