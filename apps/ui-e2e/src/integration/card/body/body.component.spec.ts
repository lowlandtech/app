describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=bodycomponent--primary'));

  it('should render the component', () => {
    cy.get('scx-ui-card-body').should('exist');
  });
});
