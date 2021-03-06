import React, { useEffect } from "react"
import { Typography } from "@material-ui/core"
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
  HeaderFilter, FilterRow, Lookup,
} from 'devextreme-react/tree-list';
import { users } from "../../../api/mock/users"
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { BASE_URL } from '../../../core/constants';
import { connect } from 'react-redux';
import { IAppState } from '../../../core/mainReducer';


const User = ({ user }: { user: any }) => {

  const onCellPrepared = (e: any) => {
    if (e.column.command === 'edit') {
      let addLink = e.cellElement.querySelector('.dx-link-add');

      if (addLink) {
        addLink.remove();
      }
    }
  }

  const allowedPageSizes = [20, 50, 100];
  const userData: any = localStorage.getItem('auth')
  const token: any = userData ? JSON.parse(userData).token : user?.token ?? ""
  const usersData: any = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Users?page=1&pageSize=2000`,
    insertUrl: `${BASE_URL}api/Users`,
    updateUrl: `${BASE_URL}api/Users`,
    deleteUrl: `${BASE_URL}api/Users`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: 'Bearer ' + token,
      };
    }
  });
  useEffect(() => {
    document.title = 'Пользователи'
  })

  const rolesData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Roles?page=1&pageSize=2000`,
    onBeforeSend: function (method, ajaxOptions) {
      ajaxOptions.headers = {
        Authorization: `Bearer ${token}`,
      };
    }
  });

  return (
    <Typography>
      <TreeList
        id="roles"
        dataSource={usersData}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        columnHidingEnabled={true}
        keyExpr="id"
        onCellPrepared={onCellPrepared}
        style={{ height: '85vh' }}
      >

        <Scrolling mode="standard" />

        <SearchPanel visible={true} />
        <HeaderFilter visible={true} />
        {user?.role?.id !== 2 && <Editing
          allowUpdating={true}
          allowDeleting={true}
          allowAdding={true}
          mode="row"
        />
        }

        <Paging
          enabled={true}
          defaultPageSize={20}
        />
        <Pager
          showPageSizeSelector={true}
          allowedPageSizes={allowedPageSizes}
          showInfo={true}
        />
        <FilterRow visible={true} />
        <Sorting mode="multiple" />
        <Selection mode="single" />

        <Column
          caption={"Id"}
          visible={true}
          dataField={"id"}
          allowEditing={false}
          alignment={'left'}
        >
        </Column>
        <Column
          caption={"Фамилия"}
          dataField={"lastName"}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        <Column
          caption={"Имя"}
          dataField={"firstName"}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        {/*<Column*/}
        {/*    caption={"Отчество"}*/}
        {/*    dataField={"lastName"}>*/}
        {/*    <RequiredRule />*/}
        {/*</Column>*/}
        <Column
          caption={"Имя роли"}
          dataField={"roleId"}
          alignment={'left'}
        >
          <RequiredRule />
          <Lookup dataSource={rolesData} valueExpr="id" displayExpr="roleName" />
        </Column>
        <Column
          caption={"Пароль"}
          dataField={"password"}
          alignment={'left'}
        >
        </Column>
        <Column
          caption={"Заблокирован"}
          dataType="boolean"
          dataField={"isDeleted"}
          visible={true}
          alignment={'left'}
        >
        </Column>
      </TreeList>
    </Typography>
  )
}

export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user
  }
})(User)
