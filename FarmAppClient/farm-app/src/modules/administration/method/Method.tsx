import React, { useEffect } from 'react';
import { Typography } from '@material-ui/core';
import TreeList, {
  Editing,
  SearchPanel,
  Column,
  RequiredRule,
  Selection,
  Sorting,
  Scrolling,
  Paging,
  Pager,
  FilterRow,
} from 'devextreme-react/tree-list';
import { apimethods } from '../../../api/mock/apimethod';
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { BASE_URL } from '../../../core/constants';
import { connect } from 'react-redux';
import { IAppState } from '../../../core/mainReducer';

///api/ApiMethods


const Method = ({ user }: { user: any }) => {
  const allowedPageSizes = [20, 50, 100];

  const userData: any = localStorage.getItem('auth')
  const token: any = userData ? JSON.parse(userData).token : user?.token ?? ""
  const methodsData: any = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/ApiMethods?page=1&pageSize=2000`,
    insertUrl: `${BASE_URL}api/ApiMethods`,
    updateUrl: `${BASE_URL}api/ApiMethods`,
    deleteUrl: `${BASE_URL}api/ApiMethods`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: 'Bearer ' + token,
      };
    },
  });

  const onCellPrepared = (e: any) => {
    if (e.column.command === 'edit') {
      let addLink = e.cellElement.querySelector('.dx-link-add');

      if (addLink) {
        addLink.remove();
      }
    }
  };

  useEffect(() => {
    document.title = 'Методы'
  })
  return (
    <Typography>
      <TreeList
        id="apimethods"
        dataSource={methodsData}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        keyExpr="id"
        style={{ height: '85vh' }}
        onCellPrepared={onCellPrepared}
        columnHidingEnabled={true}
      >
        <Scrolling mode="standard" />
        <Paging
          enabled={true}
          defaultPageSize={5} />
        <Pager
          showPageSizeSelector={true}
          allowedPageSizes={allowedPageSizes}
          showInfo={true} />
        <FilterRow visible={true} />
        <Sorting mode="multiple" />
        <Selection mode="single" />
        <SearchPanel visible={true} />
        {user?.role?.id === 1 && <Editing
          allowUpdating={true}
          allowDeleting={true}
          allowAdding={true}
          mode="row"
        />
        }
        <Column
          caption={'Номер'}
          dataType={'number'}
          visible={false}
          dataField={'id'}
          alignment={'left'}
        >
        </Column>
        <Column
          caption={'Имя метода'}
          dataType={'string'}
          dataField={'apiMethodName'}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        <Column
          caption={'Описание'}
          dataType={'string'}
          dataField={'description'}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        <Column
          caption={'Короткий адрес'}
          dataType={'string'}
          dataField={'pathUrl'}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        <Column
          caption={'Http Метод'}
          dataType={'string'}
          dataField={'httpMethod'}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        {/* <Column
          caption={'Параметры'}
          dataField={'isNotNullParam'}>
          <RequiredRule />
        </Column> */}
        <Column
          caption={'Аунтификация'}
          dataField={'isNeedAuthentication'}
          alignment={'left'}
        >
        </Column>
        <Column
          caption={'Заблокировано'}
          dataField={'isDeleted'}
          dataType="boolean"
          alignment={'left'}
        >

        </Column>
        {/* <Column
          caption={'Удалена'}
          dataType="boolean"
          dataField={'isDeleted'}>
        </Column> */}
      </TreeList>
    </Typography>
  );
};


export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user,
  };
})(Method);
