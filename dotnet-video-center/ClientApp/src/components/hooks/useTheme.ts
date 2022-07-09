import { useCallback } from 'react';
import { useSearchParams } from 'react-router-dom';
import { useEffectOnce, useLocalStorage, useMount } from 'react-use';

const key = 'theme';
export function useTheme() {
  const [cache, setCache] = useLocalStorage(key, 'light');
  const [searchParam, setSearchParam] = useSearchParams(cache);
  const setTheme = useCallback(
    (v: string) => {
      setCache(v);
      setSearchParam({ theme: v }, { replace: true });
    },
    [setCache, setSearchParam]
  );
  const theme = searchParam.get('theme') || cache;
  useMount(() => {
    setTheme(cache || 'light');
  });
  return { theme, setTheme };
}
