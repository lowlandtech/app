describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=titlecomponent--primary'));

  it('should render the component', () => {
    cy.get('scx-ui-card-title').should('exist');
  });
});
