import { AsideToggled } from './aside.actions';
import { AsideState, initialState, reducer } from './aside.reducer';

describe('Aside Reducer', () => {

  describe('valid Aside actions ', () => {
    it('should return set the list of known Aside', () => {
      const action = new AsideToggled();
      const result: AsideState = reducer(initialState, action);

      expect(result.toggled).toBe(true);
    });
  });

  describe('unknown action', () => {
    it('should return the previous state', () => {
      const action = {} as any;
      const result = reducer(initialState, action);

      expect(result).toBe(initialState);
    });
  });
});
