import React from 'react';
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


const Logs = ({ user }: { user: any }) => {
  const allowedPageSizes = [20, 50, 100];
  const logsData: any = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Logs?page=1&pageSize=2000`,
    // insertUrl: `${BASE_URL}api/ApiMethods`,
    // updateUrl: `${BASE_URL}api/ApiMethods`,
    // deleteUrl: `${BASE_URL}api/ApiMethods`,
    onBeforeSend: function(method, ajaxOptions) {
      ajaxOptions.xhrFields = { withCredentials: false };
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
  return (
      <Typography
      >
        <TreeList
          id="apimethods"
          dataSource={logsData}
          showRowLines={true}
          showBorders={true}
          columnAutoWidth={true}
          keyExpr="id"
          onCellPrepared={onCellPrepared}
          columnHidingEnabled={true}
        >
          <Scrolling mode="standard"/>
          <Paging
            enabled={true}
            defaultPageSize={5}/>
          <Pager
            showPageSizeSelector={true}
            allowedPageSizes={allowedPageSizes}
            showInfo={true}/>
          <FilterRow visible={true}/>
          <Sorting mode="multiple"/>
          <Selection mode="single"/>
          <SearchPanel visible={true}/>
          {/*{user?.role?.id === 1 && <Editing*/}
          {/*  allowUpdating={true}*/}
          {/*  allowDeleting={true}*/}
          {/*  allowAdding={true}*/}
          {/*  mode="row"*/}
          {/*/>*/}
          {/*}*/}
          <Column
            caption={'Номер'}
            dataType={'number'}
            visible={false}
            dataField={'id'}>
          </Column>
          <Column
            caption={'Тип лога'}
            dataType={'string'}
            dataField={'logType'}>
          </Column>
          <Column
            caption={'Дата'}
            // dataType={'data'}
            dataField={'createDate'}>
          </Column>
          <Column
            caption={'Id пользователя'}
            dataType={'number'}
            dataField={'userId'}>
          </Column>
          <Column
            caption={'Роль пользователя'}
            dataType={'number'}
            dataField={'roleId'}>
          </Column>
          <Column
            caption={'Код статуса'}
            dataType={'string'}
            dataField={'statusCode'}>
          </Column>
          <Column
            caption={'Код статуса'}
            dataType={'string'}
            dataField={'pathUrl'}>
          </Column>
          <Column
            caption={'HTTP метод'}
            dataType={'string'}
            dataField={'httpMethod'}>
          </Column>
          <Column
            caption={'Header'}
            dataType={'string'}
            dataField={'header'}>
          </Column>
          <Column
            caption={'Body'}
            dataType={'string'}
            dataField={'body'}>
          </Column>
          <Column
            caption={'Exception'}
            dataType={'string'}
            dataField={'exception'}>
          </Column>
          {/*<Column*/}
          {/*  caption={'Параметры'}*/}
          {/*  dataField={'isNotNullParam'}>*/}
          {/*  <RequiredRule/>*/}
          {/*</Column>*/}
          {/*<Column*/}
          {/*  caption={'Аунтификация'}*/}
          {/*  dataField={'isNeedAuthentication'}>*/}
          {/*  <RequiredRule/>*/}
          {/*</Column>*/}
          {/*<Column*/}
          {/*  caption={'Удалена'}*/}
          {/*  dataType="boolean"*/}
          {/*  dataField={'isDeleted'}>*/}
          {/*</Column>*/}
        </TreeList>
      </Typography>

  );
};


export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user,
  };
})(Logs);
