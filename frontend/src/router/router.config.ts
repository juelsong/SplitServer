import { RouteRecordRaw } from "vue-router";
/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap: RouteRecordRaw[] = [
  {
    path: "/redirect",
    component: () => import("@/layout/index.vue"),
    meta: { hidden: true },
    children: [
      {
        path: "/redirect/:path(.*)",
        component: () => import("@/views/redirect/index.vue"),
      },
    ],
  },
  {
    path: "/",
    component: () => import("@/layout/index.vue"),
    redirect:
      process.env.NODE_ENV === "client"
        ? "/inspectionExecution/inspectionRecord"
        : "/dashboard",
    children:
      process.env.NODE_ENV === "client"
        ? []
        : [
            {
              path: "dashboard",
              component: () => import("@/views/dashboard/simpleDashboard.vue"),
              name: "Dashboard",
              meta: { code: "menu.dashboard", icon: "dashboard", affix: true },
            },
          ],
  },
  {
    path: "/login",
    component: () => import("@/views/login/index.vue"),
    meta: { hidden: true },
  },
  {
    path: "/profile",
    component: () => import("@/layout/index.vue"),
    redirect: "/profile/index",
    meta: { hidden: true },
    children: [
      {
        path: "index",
        component: () => import("@/views/profile/index.vue"),
        name: "Profile",
        meta: { code: "label.navbar.profile", icon: "user", noCache: true },
      },
    ],
  },
  {
    path: "/screen",
    component: () => import("@/views/screen/index.vue"),
    meta: { hidden: true },
  },
  {
    path: "/404",
    component: () => import("@/views/error-page/404.vue"),
    meta: { hidden: true },
  },
  {
    path: "/401",
    component: () => import("@/views/error-page/401.vue"),
    meta: { hidden: true },
  },
  {
    path: "/error",
    component: () => import("@/layout/index.vue"),
    redirect: "noRedirect",
    name: "ErrorPages",
    meta: {
      title: "Error Pages",
      icon: "404",
      hidden: true,
    },
    // children: new Array(21).fill(1).map((_, idx) => {
    //     let name = (Array(3).join("0") + (idx + 1)).slice(-3);
    //     return {
    //         path: `${name}`,
    //         component: {
    //             template: `<div>${name}</div>`,
    //         },
    //         name: `Page${name}`,
    //         meta: { title: `${name}`, noCache: true }
    //     }
    // }),
    children: [
      {
        path: "401",
        component: () => import("@/views/error-page/401.vue"),
        name: "Page401",
        meta: { title: "401", noCache: true },
      },
      {
        path: "404",
        component: () => import("@/views/error-page/404.vue"),
        name: "Page404",
        meta: { title: "404", noCache: true },
      },
    ],
  },
  // 404 page must be placed at the end !!!
  // { path: '*', redirect: '/404', hidden: true }
];

const asyncRoutesArray = [
  {
    path: "/system",
    component: () => import("@/layout/index.vue"),
    name: "system",
    redirect: "noRedirect",
    meta: {
      code: "menu.system",
      icon: "system",
    },
    children: [
      {
        path: "security",
        component: () => import("@/views/security/Security.vue"),
        name: "Security",
        meta: {
          code: "menu.security",
          icon: "security",
          permissions: ["security"],
        },
      },
      {
        path: "log",
        component: () => import("@/views/system/Log.vue"),
        name: "Log",
        meta: {
          code: "menu.log",
          icon: "log",
          permissions: ["log"],
        },
      },
      {
        path: "settings",
        component: () => import("@/views/system/Settings/index.vue"),
        name: "Settings",
        meta: {
          code: "menu.settings",
          icon: "setting",
          permissions: ["settings"],
        },
      },
    ],
  },
  {
    path: "/deviceTMP",
    component: () => import("@/layout/index.vue"),
    name: "DeviceTMP",
    meta: {
      icon: "auditPrompt",
    },
    children: [
      {
        path: "/deviceTMP",
        component: () => import("@/views/dashboard/index.vue"),
        name: "DeviceTMP",
        meta: {
          code: "menu.deviceTMP",
          icon: "auditPrompt",
          permissions: ["region"],
        },
      },
    ],
  },
];

function set_parent_permission(route: RouteRecordRaw) {
  if (
    route.meta &&
    !route.meta.permissions &&
    route.children &&
    route.children.length > 0
  ) {
    route.meta.permissions = [];
    route.children.forEach((sr) => {
      set_parent_permission(sr);
      if (sr.meta && sr.meta.permissions && route.meta) {
        const childPermissions = sr.meta.permissions as Array<string>;
        const routePermissions = route.meta.permissions as Array<string>;
        routePermissions.push(...childPermissions);
      }
    });
  }
}

asyncRoutesArray.forEach((r) => {
  set_parent_permission(r);
});

export const asyncRoutes = asyncRoutesArray;

export default {
  constantRouterMap,
};
