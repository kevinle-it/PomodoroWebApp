import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../NavMenu';
import './styles.scss';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div className="layout__wrapper">
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
