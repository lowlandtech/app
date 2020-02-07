describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=toolbarcomponent--primary'));

  it('should render the component', () => {
    cy.get('scx-ui-card-toolbar').should('exist');
  });
});
