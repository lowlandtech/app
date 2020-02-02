import { asideQuery } from './aside.selectors';

describe('Aside Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  let storeState;

  beforeEach(() => {
    storeState = {
      aside: {
        error: ERROR_MSG,
        toggled: true
      }
    };
  });

  describe('Aside Selectors', () => {

    it("getToggled() should return the current 'toggled' status", () => {
      const result = asideQuery.getToggled(storeState);

      expect(result).toBe(true);
    });

    it("getError() should return the current 'error' storeState", () => {
      const result = asideQuery.getError(storeState);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
